from datetime import datetime, timedelta
from airflow import DAG
from airflow.operators.bash import BashOperator

# устанавливаем аргументы для DAG
default_args = {
    'owner': 'airflow',
    'depends_on_past': False,
    'start_date': datetime(2023, 2, 26),
    'retries': 1,
    'retry_delay': timedelta(minutes=1),
}

# создаем DAG
dag = DAG(
    'json_loader',
    default_args=default_args,
    description='The work DAG to load JSON files',
    schedule_interval=timedelta(days=1),
)

# создаем задачу для обработки JSON файлов
load_json_task = BashOperator(
    task_id='load_json',
    # Для Windows
    # bash_command='python D:\\json_load\\loads_json.py',
    # Для докера
    bash_command='python /opt/airflow/json_load/loads_json.py',
    dag=dag,
)

# устанавливаем порядок выполнения задач
load_json_task
