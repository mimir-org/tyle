import { useDebounce } from "common/hooks/useDebounce";
import { Dispatch, useState } from "react";

/**
 * Hook for combining useState and useDebounce.
 * Exposes state value, state setter and debounced state value.
 *
 * @example
 * const Example = () => {
 *  [searchInput, setSearchInput, debouncedSearchInput] = useDebounceState('', 300);
 *
 *  useEffect(() => {
 *    expensiveApiCall(debouncedSearchInput);
 *  }, [debouncedSearchInput]);
 *
 *  return (
 *    <input value={searchInput} onChange={(e) => setSearchInput(e.target.value)}>
 *  );
 * };
 *
 *
 * @param initialState
 * @param delay debounce interval in milliseconds (default: 275)
 */
export const useDebounceState = <T>(
  initialState: T,
  delay = 275
): [state: T, setState: Dispatch<T>, debouncedState: T] => {
  const [state, setState] = useState<T>(initialState);
  const debouncedState = useDebounce(state, delay);
  return [state, setState, debouncedState];
};
