export const toggleDarkTheme = () => {
  localStorage.getItem("theme") === "tyleDark"
    ? localStorage.setItem("theme", "tyleLight")
    : localStorage.setItem("theme", "tyleDark");

  window.dispatchEvent(new Event("storage"));
};
