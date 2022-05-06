import { Dispatch, useEffect, useState } from "react";
import { useMediaQuery } from "./useMediaQuery";

/**
 * Hook for checking the users preferred theme
 *
 * Checks if the users has preference stored in their local storage,
 * falls back to media query 'prefers-color-scheme' if not present.
 *
 * @param initial theme state
 */
export function usePrefersTheme(initial = "light"): [colorTheme: string, setColorTheme: Dispatch<string>] {
  const [colorTheme, setColorTheme] = useState(initial);
  const preferDark = useMediaQuery("(prefers-color-scheme: dark)");

  useEffect(() => {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme && ["dark", "light"].includes(savedTheme)) {
      setColorTheme(savedTheme);
    } else if (preferDark) {
      setColorTheme("dark");
    } else if (!preferDark) {
      setColorTheme("light");
    }
  }, [preferDark]);

  return [colorTheme, setColorTheme];
}
