# Step 1: Build the React app
FROM node:21 AS build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application files
COPY . .

# Build the React app for production
RUN npm run build

# Step 2: Serve the app using Node.js
FROM node:21

# Set the working directory
WORKDIR /app

# Copy the build files from the previous stage
COPY --from=build /app/build ./build

# Install serve, a static file server for Node.js
RUN npm install -g serve

# Expose port 3000
EXPOSE 3000

# Command to run the app
CMD ["serve", "-s", "build", "-l", "3000"]
