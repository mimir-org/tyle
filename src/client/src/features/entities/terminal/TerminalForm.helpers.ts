import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "external/sources/terminal/terminal.queries";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";
import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { RdlClassifier } from "common/types/common/rdlClassifier";
import { TerminalView } from "common/types/terminals/terminalView";
import { Direction } from "common/types/terminals/direction";
import { RdlMedium } from "common/types/terminals/rdlMedium";
import { InfoItem } from "common/types/infoItem";
import { RdlPurpose } from "common/types/common/rdlPurpose";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal(id);
  return mode === "edit" ? updateMutation : createMutation;
};

export interface TerminalFormFields extends Omit<TerminalTypeRequest, "description" | "classifierIds"> {
  description: string;
  classifiers: RdlClassifier[];
}

export const toTerminalFormFields = (terminal: TerminalView): TerminalFormFields => ({
  ...terminal,
  description: terminal.description ?? "",
  purposeId: terminal.purpose?.id,
  mediumId: terminal.medium?.id,
  attributes: terminal.attributes.map((x) => ({ attributeId: x.attribute.id, minCount: x.minCount, maxCount: x.maxCount }))
});

export const toTerminalTypeRequest = (terminalFormFields: TerminalFormFields): TerminalTypeRequest => ({
  ...terminalFormFields,
  description: terminalFormFields.description ? terminalFormFields.description : undefined,
  classifierIds: terminalFormFields.classifiers.map(x => x.id)
});

export const createDefaultTerminalFormFields = (): TerminalFormFields => ({
  name: "",
  description: "",
  classifiers: [],
  purposeId: undefined,
  notation: undefined,
  symbol: undefined,
  aspect: undefined,
  mediumId: undefined,
  qualifier: Direction.Bidirectional,
  attributes: []
});

export const mediumInfoItem = (medium: RdlMedium): InfoItem => ({
  id: medium.id.toString(),
  name: medium.name,
  descriptors: {
    Description: medium.description,
    IRI: medium.iri,
  },
});

export const classifierInfoItem = (classifier: RdlClassifier): InfoItem => ({
  id: classifier.id.toString(),
  name: classifier.name,
  descriptors: {
    Description: classifier.description,
    IRI: classifier.iri,
  },
});

export const purposeInfoItem = (purpose: RdlPurpose): InfoItem => ({
  id: purpose.id.toString(),
  name: purpose.name,
  descriptors: {
    Description: purpose.description,
    IRI: purpose.iri,
  },
});