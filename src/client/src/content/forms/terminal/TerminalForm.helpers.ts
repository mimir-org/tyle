import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useParams } from "react-router-dom";
import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "../../../data/queries/tyle/queriesTerminal";
import { TerminalFormMode } from "./types/terminalFormMode";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (mode?: TerminalFormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  return mode === "edit" ? updateMutation : createMutation;
};

export const prepareAttributes = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return attributes.sort((a, b) => a.discipline - b.discipline);
};
