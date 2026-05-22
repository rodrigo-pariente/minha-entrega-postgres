"""Module for Minha Entrega API requests"""

from typing import Any
import requests
from core.die import die
from core.dto_converter import order_dto_to_order
from models.order import Order
from collections.abc import Callable

url = "http://localhost:5089/orders"

def safe_request(func: Callable) -> Callable:
    """Exception handling for http requests"""
    def inner(*args) -> Any:
        try:
            return func(*args)
        except requests.exceptions.ConnectionError:
            die("Não foi possível conectar com o banco de dados")
    return inner

@safe_request
def get_orders() -> requests.Response:
    orders: list[Order] = list(map(order_dto_to_order, requests.get(url).json()))
    return orders

@safe_request
def get_order_by_name(name: str) -> tuple[Order | None, bool]:
    response = requests.get(f"{url}/name/{name}")
    if response:
        return order_dto_to_order(response.json()), True
    return None, False

@safe_request
def post_order(code: str, name: str) -> requests.Response:
    return requests.post(url, json = {"code": code, "name": name})

@safe_request
def delete_order(code: str) -> requests.Response:
    return requests.delete(f"{url}/{code}")

@safe_request
def refresh_order(code: str) -> requests.Response:
    return requests.post(f"{url}/{code}/refresh")

@safe_request
def dumb_refresh_all_orders() -> list[Order] | None:
    response = requests.post(f"{url}/dumb_refresh")
    if response:
        return list(map(order_dto_to_order, response.json())), True
    return None, False

@safe_request
def refresh_all_orders() -> list[Order] | None:
    response = requests.post(f"{url}/refresh")
    if response:
        return list(map(order_dto_to_order, response.json())), True
    return None, False

@safe_request
def put_order(code: str, name: str) -> requests.Response:
    return requests.put(f"{url}/{code}", json={"name": name})
