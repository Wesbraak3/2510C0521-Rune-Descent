FROM mongo:7.0

# Copy init scripts
COPY initdb/ /docker-entrypoint-initdb.d/

# Make sure shell scripts are executable
RUN chmod +x /docker-entrypoint-initdb.d/*.sh