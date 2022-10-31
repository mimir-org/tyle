import { useEffect, useState } from "react";

/**
 * Hook for debouncing the update of a state.
 *
 * @see https://usehooks-ts.com/react-hook/use-debounce
 * @param state
 * @param delay
 */
export const useDebounce = <T>(state: T, delay?: number): T => {
  const [debouncedState, setDebouncedState] = useState<T>(state);

  useEffect(() => {
    const timer = setTimeout(() => setDebouncedState(state), delay || 500);
    return () => clearTimeout(timer);
  }, [state, delay]);

  return debouncedState;
};
