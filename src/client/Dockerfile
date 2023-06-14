FROM node:current-alpine3.17 AS base
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

FROM nginx:stable-alpine AS final
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
COPY start.sh .
RUN chmod +x start.sh
EXPOSE 80
ENTRYPOINT "./start.sh" "TYPELIBRARY_ENV" "__TYPELIBRARY_ENV__" "/usr/share/nginx/html/index.html"