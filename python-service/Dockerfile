FROM python:3.9-slim

WORKDIR /app

RUN pip install flask==2.0.3 werkzeug==2.0.3 psycopg2-binary==2.9.3

COPY . .

CMD ["python", "app.py"]