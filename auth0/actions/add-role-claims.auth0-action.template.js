/**
 * Auth0 Action: Post Login
 *
 * Fires after a user logs in and adds their Auth0 roles to the access token
 * under a namespaced custom claim that the frontend and backend can both read.
 *
 * Required secrets (set in Auth0 dashboard -> Actions -> this action -> Secrets):
 *   API_AUDIENCE  - Auth0 API identifier / audience, e.g. https://UsersShiftsApi/
 *
 * Trigger:
 *   Auth0 Dashboard -> Actions -> Flows -> Login
 *   The API_AUDIENCE secret should match the Auth0 API identifier used by the
 *   frontend and C# API.
 */
exports.onExecutePostLogin = async (event, api) => {
    if (!event.secrets.API_AUDIENCE) {
        throw new Error('Missing required Auth0 Action secret: API_AUDIENCE');
    }

    const namespace = `${event.secrets.API_AUDIENCE.replace(/\/+$/, '')}/`;
    const roles = event.authorization?.roles ?? [];

    api.accessToken.setCustomClaim(`${namespace}roles`, roles);
};
