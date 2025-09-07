FROM python:3.11-slim

WORKDIR /app
COPY 02_seed.py .

RUN pip install psycopg2-binary faker

CMD ["python", "02_seed.py"]
