import { Aspect } from "@mimirorg/typelibrary-types";
import { useGetPurposes } from "../../../../data/queries/tyle/queriesPurpose";
import { getValueLabelObjectsFromEnum } from "../../../../utils/getValueLabelObjectsFromEnum";
import { FilterGroup } from "../../types/filterGroup";

export const useGetFilterGroups = (): FilterGroup[] => [getEntityFilters(), getAspectFilters(), useGetPurposeFilters()];

const useGetPurposeFilters = (): FilterGroup => {
  const purposeQuery = useGetPurposes();

  return {
    name: "Purpose",
    filters: purposeQuery.data?.map((p) => ({ key: "purposeName", label: p.name, value: p.name })),
  };
};

const getAspectFilters = (): FilterGroup => {
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  return {
    name: "Aspect",
    filters: aspectOptions.map((a) => ({
      key: "aspect",
      label: a.label,
      value: a.value.toString(),
    })),
  };
};

const getEntityFilters = (): FilterGroup => ({
  name: "Entity",
  filters: [
    {
      key: "kind",
      label: "Aspect object",
      value: "NodeLibCm",
    },
    {
      key: "kind",
      label: "Interface",
      value: "InterfaceLibCm",
    },
    {
      key: "kind",
      label: "Terminal",
      value: "TerminalLibCm",
    },
    {
      key: "kind",
      label: "Transport",
      value: "TransportLibCm",
    },
  ],
});
