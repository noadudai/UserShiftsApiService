#!/usr/bin/env node
/**
 * check-and-regenerate-client.js
 *
 * Checks if the C# API's openapi.json has changed since the TypeScript client
 * was last generated. If it has (or if the client doesn't exist yet), regenerates
 * the client into packages/scheduler-backend-client/.
 *
 * Run automatically as a "predev" hook in frontend/front/package.json.
 * Run manually via the Cursor task "Force regenerate API client" (passes --force).
 *
 * Usage:
 *   node scripts/check-and-regenerate-client.js          # only regenerates if changed
 *   node scripts/check-and-regenerate-client.js --force  # always regenerates
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');
const crypto = require('crypto');

const ROOT = path.resolve(__dirname, '..');
const OPENAPI_JSON = path.join(ROOT, 'backend-csharp', 'UserShiftsApiService', 'openapi.json');
const CLIENT_DIR = path.join(ROOT, 'packages', 'scheduler-backend-client');
const HASH_FILE = path.join(CLIENT_DIR, '.openapi-hash');
const FORCE = process.argv.includes('--force');

function hashFile(filePath) {
    const content = fs.readFileSync(filePath);
    return crypto.createHash('sha256').update(content).digest('hex');
}

function buildDotnet() {
    console.log('[client-gen] Building C# project to refresh openapi.json...');
    execSync('dotnet build', {
        cwd: path.join(ROOT, 'backend-csharp', 'UserShiftsApiService'),
        stdio: 'inherit',
    });
}

function generateClient() {
    console.log('[client-gen] Generating TypeScript client from openapi.json...');

    // Uses Docker to run openapi-generator-cli — no Java install required.
    // The ROOT is mounted as /local inside the container.
    const openApiJsonContainer = '/local/backend-csharp/UserShiftsApiService/openapi.json';
    const clientDirContainer = '/local/packages/scheduler-backend-client';

    execSync(
        `docker run --rm \
            -v "${ROOT}:/local" \
            openapitools/openapi-generator-cli generate \
            -i "${openApiJsonContainer}" \
            -g typescript-axios \
            --additional-properties=npmName=@noadudai/scheduler-backend-client \
            --additional-properties=npmVersion=0.0.0-local \
            --additional-properties=supportsES6=true \
            -o "${clientDirContainer}"`,
        { cwd: ROOT, stdio: 'inherit' }
    );

    console.log('[client-gen] Installing client dependencies...');
    execSync('npm install', { cwd: CLIENT_DIR, stdio: 'inherit' });

    console.log('[client-gen] Building client...');
    execSync('npm run build', { cwd: CLIENT_DIR, stdio: 'inherit' });
}

function saveHash(hash) {
    fs.writeFileSync(HASH_FILE, hash, 'utf8');
}

function loadHash() {
    try {
        return fs.readFileSync(HASH_FILE, 'utf8').trim();
    } catch {
        return null;
    }
}

// ---- Main ----

if (!fs.existsSync(OPENAPI_JSON)) {
    console.log('[client-gen] openapi.json not found — building C# project first...');
    buildDotnet();
}

if (!fs.existsSync(OPENAPI_JSON)) {
    console.error('[client-gen] ERROR: openapi.json still not found after build. Is the C# project set up correctly?');
    process.exit(1);
}

const currentHash = hashFile(OPENAPI_JSON);
const savedHash = loadHash();
const clientExists = fs.existsSync(path.join(CLIENT_DIR, 'dist'));

if (!FORCE && clientExists && currentHash === savedHash) {
    console.log('[client-gen] Client is up to date — skipping regeneration.');
    process.exit(0);
}

if (FORCE) {
    console.log('[client-gen] --force flag set, regenerating...');
} else if (!clientExists) {
    console.log('[client-gen] Client not built yet — generating for the first time...');
} else {
    console.log('[client-gen] openapi.json has changed — regenerating client...');
}

generateClient();
saveHash(currentHash);
console.log('[client-gen] Done. Client is ready.');
