import { useEffect, useState } from "react";
import { useMediaQuery } from "./useMediaQuery";

interface UsePrefersThemeOptions {
  storageOnly: boolean;
}

/**
 * Hook for checking the users preferred theme
 *
 * Checks if the users has preference stored in their local storage,
 * falls back to media query 'prefers-color-scheme' if not present.
 *
 * @param initial theme state
 * @param storageOnly if set to true hook will not use the 'prefers-color-scheme' media query
 */
export function usePrefersTheme(
  initial: string,
  { storageOnly = false }: UsePrefersThemeOptions
): [colorTheme: string] {
  const [colorTheme, setColorTheme] = useState(initial);
  const preferDark = useMediaQuery("(prefers-color-scheme: dark)");

  useEffect(() => {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme && ["dark", "light"].includes(savedTheme)) {
      setColorTheme(savedTheme);
    } else if (!storageOnly && preferDark) {
      setColorTheme("dark");
    } else if (!storageOnly && !preferDark) {
      setColorTheme("light");
    }
  }, [preferDark, storageOnly]);

  return [colorTheme];
}
