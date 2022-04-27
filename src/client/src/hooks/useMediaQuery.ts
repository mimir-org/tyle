import { useEffect, useState } from "react";

/**
 * Hook for listening to media query changes
 *
 * @example
 * // Check if the users prefers a dark theme
 * const prefersDark = useMediaQuery("(prefers-color-scheme: dark)");
 *
 * @param query media query to listen to
 */
export function useMediaQuery(query: string) {
  const [matches, setMatches] = useState(false);

  useEffect(() => {
    const media = window.matchMedia(query);
    if (media.matches !== matches) {
      setMatches(media.matches);
    }
    const listener = () => {
      setMatches(media.matches);
    };
    media.addEventListener("change", listener);
    return () => media.removeEventListener("change", listener);
  }, [matches, query]);

  return matches;
}
