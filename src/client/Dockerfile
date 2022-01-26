FROM node:13.12.0-alpine AS base
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json package-lock.json ./
RUN npm ci --silent
COPY . .

FROM base as lint
RUN npm run lint

FROM base as format
RUN npm run format:check

FROM base as test
ENV CI=true
RUN npm test

FROM base as build
RUN npm run build

FROM nginx:stable-alpine as final
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]