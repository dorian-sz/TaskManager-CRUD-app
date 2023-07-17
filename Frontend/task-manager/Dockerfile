FROM node:18-alpine 

EXPOSE 3000

WORKDIR /app

COPY . .

RUN npm ci 

RUN npm run build

ENV NODE_ENV production

# Start the app
CMD [ "npm", "start" ]