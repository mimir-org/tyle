import { Link } from "../../types/link";

export const getCreateMenuLinks = (): Link[] => {
  return [
    {
      name: "Aspect object",
      path: "/form/node",
    },
    {
      name: "Interface",
      path: "/form/interface",
    },
    {
      name: "Terminal",
      path: "/form/terminal",
    },
    {
      name: "Transport",
      path: "/form/transport",
    },
  ];
};

export const useFilterState = (initial: Filter[]): [filters: Filter[], toggleFilter: (filter: Filter) => void] => {
  const [filters, setFilters] = useState<Filter[]>(initial);

  const toggleFilter = (filter: Filter) => {
    const filterIsActive = filters.some((x) => x.value === filter.value);

    if (filterIsActive) {
      setFilters(filters.filter((f) => f.value !== filter.value));
    } else {
      setFilters([...filters, filter]);
    }
  };

  return [filters, toggleFilter];
};

export const useSearchResults = (
  query: string,
  filters: Filter[],
  pageSize = 20
): [results: SearchResult[], totalHits: number, isLoading: boolean] => {
  const [searchItems, isLoading] = useSearchItems();
  const fuseResult = useFuse(searchItems, query, { keys: searchKeys, matchAllOnEmpty: true });

  const results = fuseResult.map((x) => x.item);
  const filtered = filterSearchResults(filters, results);
  const sliced = filtered.slice(0, pageSize);
  const mapped = mapSearchResults(sliced);

  return [mapped, filtered.length, isLoading];
};

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
  ];
};
