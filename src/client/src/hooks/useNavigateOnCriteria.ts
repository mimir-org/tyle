import { useNavigate } from "react-router-dom";

/**
 * Hook which simplifies navigation based on criteria
 *
 * @param path where to navigate to
 * @param criteria which decides if navigation should take place
 */
export const useNavigateOnCriteria = (path: string, ...criteria: boolean[]) => {
  const navigate = useNavigate();
  if (criteria.every((c) => c)) {
    navigate(path);
  }
};
