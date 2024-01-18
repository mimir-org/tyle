import { useEffect, useState } from "react";

/**
 * Hook for checking the users preferred theme
 *
 * Checks if the users has preference stored in their local storage.
 * Hook will respond to changes in localStorage via event listeners.
 *
 * @param initial theme state
 */
export function usePrefersTheme(initial: string): [colorTheme: string] {
  const [colorTheme, setColorTheme] = useState(initial);

  useEffect(() => {
    function setPreferredTheme() {
      const savedTheme = localStorage.getItem("theme");
      if (savedTheme && ["dark", "light"].includes(savedTheme)) {
        setColorTheme(savedTheme);
      }
    }

    setPreferredTheme();
    window.addEventListener("storage", setPreferredTheme);
    return () => window.removeEventListener("storage", setPreferredTheme);
  }, []);

  return [colorTheme];
}
