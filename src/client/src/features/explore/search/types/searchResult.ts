import {
  AspectObjectLibCm,
  AttributeLibCm,
  QuantityDatumLibCm,
  RdsLibCm,
  TerminalLibCm,
  UnitLibCm,
} from "@mimirorg/typelibrary-types";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { TerminalItem } from "common/types/terminalItem";
import { AttributeItem } from "../../../../common/types/attributeItem";

export type SearchResult = AspectObjectItem | TerminalItem | AttributeItem;

export type SearchResultRaw =
  | AspectObjectLibCm
  | TerminalLibCm
  | AttributeLibCm
  | UnitLibCm
  | QuantityDatumLibCm
  | RdsLibCm;
