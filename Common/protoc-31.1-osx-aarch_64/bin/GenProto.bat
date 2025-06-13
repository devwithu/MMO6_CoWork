#!/bin/bash

# 프로토콜 파일을 기반으로 C# 코드 생성
protoc -I=./ --csharp_out=./ ./Protocol.proto
if [ $? -ne 0 ]; then
  echo "protoc 실행 실패"
  read -p "엔터를 누르면 종료합니다..."
  exit 1
fi

# 패킷 생성기 실행
../../../Server/PacketGenerator/bin/PacketGenerator ./Protocol.proto

# 파일 복사
cp -f Protocol.cs "../../../Client/Assets/Scripts/Packet/"
cp -f Protocol.cs "../../../Server/Server/Packet/"
cp -f ClientPacketManager.cs "../../../Client/Assets/Scripts/Packet/"
cp -f ServerPacketManager.cs "../../../Server/Server/Packet/"
