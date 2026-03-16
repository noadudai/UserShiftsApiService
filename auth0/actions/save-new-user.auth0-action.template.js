/**
 * Auth0 Action: Post User Registration
 *
 * Fires after a new user registers. Sends the user's ID and email to the
 * C# API so they can be persisted in the database.
 *
 * Required secrets (set in Auth0 dashboard → Actions → this action → Secrets):
 *   HMAC_SECRET  — must match Auth0:HMAC_SECRET in the C# API config
 *   API_URL      — base URL of the C# API, no trailing slash
 *                  Local dev:   https://<your-id>.ngrok-free.app
 *                  Production:  https://your-deployed-api.com
 *
 * When ngrok restarts and gives you a new URL, only update the API_URL secret
 * value in the dashboard — no code change or redeploy needed.
 */
exports.onExecutePostUserRegistration = async (event, api) => {
    const crypto = require('crypto');

    const data = JSON.stringify({
        user_id: event.user.user_id,
        user_email: event.user.email,
    });

    const signature = crypto
        .createHmac('sha256', event.secrets.HMAC_SECRET)
        .update(data)
        .digest('hex');

    const response = await fetch(`${event.secrets.API_URL}/auth0-maintenance/save-new-user`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-Signature': signature,
        },
        body: data,
    });

    console.log('save-new-user response status:', response.status);
};
