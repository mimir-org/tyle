import { useQuery } from "@tanstack/react-query";
import { purposeApi } from "external/sources/purpose/purpose.api";

const keys = {
  all: ["purposes"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetPurposes = () => useQuery(keys.lists(), purposeApi.getPurposes);
