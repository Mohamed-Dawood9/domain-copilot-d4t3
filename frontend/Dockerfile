FROM node:20 AS build
WORKDIR /app
# We will copy package.json when Angular is scaffolded
# For now, it's just a shell that passes build
COPY . .

FROM nginx:alpine
# When Angular is scaffolded, we will copy the dist folder
# COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html
EXPOSE 80
