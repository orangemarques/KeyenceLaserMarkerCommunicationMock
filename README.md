# KeyenceLaserMarkerCommunicationMock
Keyence Laser Marker Communication Mock, é uma aplicação que "simula" a resposta da impressora laser da Keyence, facilitando assim o desenvolvimento/teste de aplicações sem a necessidade de testar diretamente na impressora laser da Keyence.

# Version 0.0.2
- Repostas com RX,OK,Title {1..n}[cr] para o comando:
  - RX,PRG={1..n},Title

# Version 0.0.1
- Servidor socket ip local, porta 50002;
- Repostas com WX,OK[cr] para os comandos:
  - WX,ProgramNo=
  - WX,PRG=
  - WX,StartMarking
  - WX,ErrorClear
