import { Aspect } from "types/common/aspect";
import { State } from "types/common/state";
import { FilterGroup } from "types/filterGroup";
import { getOptionsFromEnum } from "utils";

export const useGetFilterGroups = (): FilterGroup[] => [getTypeFilters(), getStateFilters(), getAspectFilters()];

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

const getStateFilters = (): FilterGroup => {
  const stateOptions = getOptionsFromEnum<State>(State).filter((s) => s.label !== "Deleted");

  return {
    name: "State",
    filters: stateOptions.map((s) => ({
      key: "state",
      label: s.label,
      value: s.value.toString(),
    })),
  };
};

const getTypeFilters = (): FilterGroup => ({
  name: "Type",
  filters: [
    {
      key: "kind",
      label: "Block",
      value: "BlockView",
    },
    {
      key: "kind",
      label: "Terminal",
      value: "TerminalView",
    },
    {
      key: "kind",
      label: "Attribute",
      value: "AttributeView",
    },
  ],
});
