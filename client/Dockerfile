FROM node:12.2.0-alpine
WORKDIR /app
COPY package.json ./
COPY package-lock.json  ./
COPY ./ ./
RUN npm i
RUN npm install react-scripts@3.0.1 -g
CMD ["npm", "run", "start"]
