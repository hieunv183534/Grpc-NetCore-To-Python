from  concurrent import futures
import grpc
import greet_pb2_grpc
import greet_pb2
import logging


class Greeter(greet_pb2_grpc.GreeterServicer):

    def SayHello(self, request, context):
        return greet_pb2.HelloReplyHieuNV(message='Hello from python hehe, %s!' % request.name)

def grpc_server():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    greet_pb2_grpc.add_GreeterServicer_to_server(Greeter(), server)
    server.add_insecure_port('[::]:7000')
    server.start()
    print("Server started, listening on 7000")
    server.wait_for_termination()
if __name__ == '__main__':
    logging.basicConfig()
    grpc_server()
