from flask import Flask, request, Response
import psycopg2
import os

app = Flask(__name__)

@app.route('/process', methods=['POST'])
def process_data():
    data = request.get_json()

    user_id = str(data.get('id'))
    name = str(data['name'])
    age = int(data['age'])
    processed_at = str(data['processed_at'])

    conn = psycopg2.connect(
        host=os.getenv('DB_HOST'),
        dbname=os.getenv('DB_NAME'),
        user=os.getenv('DB_USER'),
        password=os.getenv('DB_PASSWORD'),
        port=os.getenv('DB_PORT')
    )
    cursor = conn.cursor()

    cursor.execute('INSERT INTO processed_users (user_id, name, age, processed_at) VALUES (%s, %s, %s, %s)',
            (user_id, name, age, processed_at))

    conn.commit()
    cursor.close()
    conn.close()

    return Response(status=200)


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)



