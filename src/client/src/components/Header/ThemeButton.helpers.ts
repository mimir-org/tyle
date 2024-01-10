export const toggleDarkTheme = () => {
  localStorage.getItem("theme") === "dark"
    ? localStorage.setItem("theme", "light")
    : localStorage.setItem("theme", "dark");

  window.dispatchEvent(new Event("storage"));
};
