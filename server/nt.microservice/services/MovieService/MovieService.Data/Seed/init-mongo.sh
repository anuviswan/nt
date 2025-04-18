#!/bin/bash
echo "⏳ Seeding MongoDB with movies.json..."
mongoimport --host nt.movieservice.db \
            --db MovieDb \
            --collection movies \
            --type json \
            --file /services/MovieService/MovieService.Data/Seed/movies.json \
            --jsonArray
echo "✅ Seeding complete."
