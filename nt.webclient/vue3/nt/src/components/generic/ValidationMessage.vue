<template>
  <div v-bind:class="getMessageClass()">
    <div v-if="messages.length > 1">
      <ul>
        <li v-for="(msg, index) in messages" :key="index">
          <small>{{ msg }}</small>
        </li>
      </ul>
    </div>
    <div v-else>
      <small>{{ messages[0] }}</small>
    </div>
  </div>
</template>

<script>
export default {
  name: "ValidationMessage",
  props: {
    messages: {
      type: Array,
      required: true,
      default: () => [],
    },
    isError: {
      type: Boolean,
      required: true,
    },
  },

  setup(props) {
    const getMessageClass = () => {
      if (!props.messages) return;

      if (props.messages.length == 0) {
        return "d-none";
      }

      return props.isError ? "text-danger text-left" : "text-success text-left";
    };
    console.log(props);
    return {
      getMessageClass,
    };
  },
};
</script>

<style></style>
