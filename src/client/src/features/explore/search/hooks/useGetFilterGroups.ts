import { Aspect } from "@mimirorg/typelibrary-types";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { useGetPurposes } from "external/sources/purpose/purpose.queries";
import { FilterGroup } from "features/explore/search/types/filterGroup";

export const useGetFilterGroups = (): FilterGroup[] => [getEntityFilters(), getAspectFilters(), useGetPurposeFilters()];

const useGetPurposeFilters = (): FilterGroup => {
  const purposeQuery = useGetPurposes();

  return {
    name: "Purpose",
    filters: purposeQuery.data?.map((p) => ({ key: "purposeName", label: p.name, value: p.name })),
  };
};

const getAspectFilters = (): FilterGroup => {
  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);

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
      value: "AspectObjectLibCm",
    },
    {
      key: "kind",
      label: "Terminal",
      value: "TerminalLibCm",
    },
    {
      key: "kind",
      label: "Attribute",
      value: "AttributeLibCm",
    },
    {
      key: "kind",
      label: "Unit",
      value: "UnitLibCm",
    },
    {
      key: "kind",
      label: "Quantity datum",
      value: "QuantityDatumLibCm",
    },
    {
      key: "kind",
      label: "RDS",
      value: "RdsLibCm",
    },
  ],
});
