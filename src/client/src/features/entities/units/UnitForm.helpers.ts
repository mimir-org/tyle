import { useParams } from "react-router-dom";
import { useCreateUnit, useGetUnit } from "../../../external/sources/unit/unit.queries";

export const useUnitQuery = () => {
  const { id } = useParams();
  return useGetUnit(id);
};

export const useUnitMutation = () => {
  return useCreateUnit();
};
