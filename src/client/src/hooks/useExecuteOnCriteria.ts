import { useEffect } from "react";

/**
 * Hook which simplifies conditional execution based on criteria
 *
 * @param executable function to call if criteria are true
 * @param criteria which decides if navigation should take place
 */
export const useExecuteOnCriteria = (executable?: () => void, ...criteria: boolean[]) => {
  useEffect(() => {
    if (criteria.every((c) => c) && executable) {
      executable();
    }
  }, [criteria, executable]);
};
