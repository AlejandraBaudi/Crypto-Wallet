<template>
  <div class="fondo">
    <div class="saldo">
      <button class="btn-volver" @click="volverMenu">Volver al menu principal</button>
      <h1>Estado Actual de la Billetera</h1>

      <table>
        <thead>
          <tr>
            <th>Criptomoneda</th>
            <th>Cantidad</th>
            <th>Valor en ARS</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in resumen" :key="item.cryptoCode">
            <td>{{ item.cryptoCode }}</td>
            <td>{{ item.cantidad }}</td>
            <td>${{ item.valorEnpesos }}</td>
          </tr>
        </tbody>
      </table>

      <h3>Total en Pesos: ${{ totalEnPesos }}</h3>
    </div>
  </div>
</template>

<script>
export default {
  name: "saldo-actual",
  data() {
    return {
      resumen: [],
      totalEnPesos: 0
    };
  },
  mounted() {
    fetch("https://localhost:7116/wallet/status")
      .then(response => {
        if (!response.ok) throw new Error("Error al obtener estado de billetera");
        return response.json();
      })
      .then(data => {
        this.resumen = data.resumen;
        this.totalEnPesos = data.totalEnPesos;
      })
      .catch(error => {
        console.error("Error al cargar saldo:", error);
      });
  },
  methods: {
    volverMenu() {
      this.$router.push("/");
    }
  }
};
</script>