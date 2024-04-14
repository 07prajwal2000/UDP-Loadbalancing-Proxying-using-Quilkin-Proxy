# UDP Loadbalancing/Proxying using Quilkin Proxy

I made a demo using quilkin proxy (Proxy written by Google and Embark Studios in Rust specifically designed for UDP game servers to route packets to particular server based on packet inspection) and LitenetLib (A Reliable UDP library for C#.Net).

# Requirements
- Docker
- Dotnet 8

# Steps to run
- Build the docker image of the server project
- Run the command using `docker run --name udpdemo1 --net testnet -e "TRIM_TOKEN=true" --rm manu6366/udp-demo`. (I named the docker container because it will be easier to pass dns names instead of IP that's changing everytime).
- Run the Quilkin docker image using the command `docker run --name quilkin --net testnet -p "5008:5008/udp" --rm -it -v "$pwd/quilkin.yml:/etc/quilkin/quilkin.yaml" us-docker.pkg.dev/quilkin/release/quilkin:0.8.0 proxy -p 5008`. I exposed the port 5008 and mounted the config file to specific folder that quilkin searches for.
- In client, It is required to prepend the server token to each packet that should be 4bytes long.