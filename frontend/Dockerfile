# Fetch node.js version 22 the alpine (small) version.
FROM node:22-alpine As builder

WORKDIR /usr/src/app

COPY package*.json ./

# SignalR needs python.
RUN apk add --update python3 make g++\
   && rm -rf /var/cache/apk/*

RUN npm install -g @angular/cli@22.1.2

RUN npm install

COPY . .

RUN ng build --configuration production

#install nginx
FROM nginx:stable-alpine

COPY nginx.conf.prod /etc/nginx/conf.d/nginx.conf
COPY --from=builder /usr/src/app/dist/* /usr/share/nginx/html

EXPOSE 80