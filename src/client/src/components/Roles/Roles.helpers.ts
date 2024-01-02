import { Option } from "utils";

export const roleFilters: Option<string>[] = [
  { value: "-1", label: "All" },
  { value: "0", label: "None" },
  { value: "1", label: "Reader" },
  { value: "2", label: "Contributor" },
  { value: "3", label: "Reviewer" },
  { value: "4", label: "Administrator" },
];
