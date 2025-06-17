<template>
  <div class="menu-opciones">
    <button @click="$emit('navegar', 'compra-venta')">Comprar / Vender</button>
    <button @click="$emit('navegar', 'historial-movimientos')">Ver Historial</button>
    <button @click="$emit('navegar', 'saldo-actual')">Saldo Actual</button>
  </div>
</template>

<script>
export default {
  name: 'OpcionesMenu'
}
</script>

<style scoped>
.menu-opciones {
    display: flex;
    gap: 1rem;
    justify-content: center;
}

.menu-opciones button {
    padding: 0.5rem 1rem;
    background-color:rgb(134, 53, 171);
    color: white;
    border: none;
    padding: 0.5rem auto;
    font-size: 1.5rem;
    border-radius: 10px;
    cursor: pointer;
    transition: 0.3s;
}
.menu-opciones button:hover {
    background-color: rgb(156, 104, 181);
    transform: scale(1.1);
    box-shadow: 0 4px 10px rgba(101, 11, 103, 0.6);
}
</style>
