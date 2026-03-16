FROM node:lts AS build
WORKDIR /app

# Install dependencies
COPY front/package*.json ./front/
# Copy workspace root package files if present
COPY package*.json ./ 2>/dev/null || true
RUN cd front && npm install

# Build the app
COPY front/ ./front/
RUN cd front && npm run build

FROM nginx:alpine AS runtime
COPY --from=build /app/front/dist /usr/share/nginx/html

# SPA routing: redirect all requests to index.html
RUN printf 'server {\n\
    listen 80;\n\
    root /usr/share/nginx/html;\n\
    index index.html;\n\
    location / {\n\
        try_files $uri $uri/ /index.html;\n\
    }\n\
}\n' > /etc/nginx/conf.d/default.conf

EXPOSE 80
