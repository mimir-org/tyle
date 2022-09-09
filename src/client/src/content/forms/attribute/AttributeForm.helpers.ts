import { Select } from "@mimirorg/typelibrary-types";
import { useParams } from "react-router-dom";
import { useGetAttribute } from "../../../data/queries/tyle/queriesAttribute";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const showSelectValues = (attributeSelect: Select) => attributeSelect !== Select.None;
