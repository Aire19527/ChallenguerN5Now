<h1> Challenguer  N5Now </h1>

<h2> Configuraciones Base </h2>
- 1. En la solución se deben seleccionar los dos proyectos: <strong> Kafka.Consumer </strong> y <strong> N5Now </strong> (producer)

<img align="center" alt="PNG" src="https://user-images.githubusercontent.com/44555206/235299882-65e38062-9e3a-475f-b946-e73b99d51b17.png"/>

- 2. Reemplazar los valores en el archivo appsetting.json, están con los valores por default
    - a.  <strong> conif ElasticsearchServer  </strong>
    - b.  <strong> config KafkaProducer  </strong>
<img align="center" alt="PNG" src="https://user-images.githubusercontent.com/44555206/235300162-3c5546b6-4c11-4167-9172-861fcc2af869.png"/>

- 3. Configurar <strong>ConnectionStrings</strong> de SQL SERVER en el archivo  appsetting.json del proyecto <strong> N5Now </strong>

<h3>🛠 Imagen Docker N5Now</h3>

- 💻 docker build -t imgn5nowjp -f Dockerfile .
- 💻 docker run -d -p 5024:5024 -p 80:80 --name challenguerJP imgn5nowjp

<h3>🛠 Imagenes Docker Usadas </h3>

- 🌐 &nbsp; Elasticsearch
    - 💻 docker run `
          --name elasticsearch `
          --net elastic `
          -p 127.0.0.1:9200:9200 `
          -p 127.0.0.1:9300:9300 `
          -e "discovery.type=single-node" `
          docker.elastic.co/elasticsearch/elasticsearch:7.17.9
- 🌐 &nbsp; Kafka
    - 💻 curl --silent --output docker-compose.yml https://raw.githubusercontent.com/confluentinc/cp-all-in-one/6.1.1-post/cp-all-in-one/docker-compose.yml
    - 💻 configurar el topic en la siguiente url: http://localhost:9021/clusters


<h3> Gracias :) !🤝🏻 </h3>
