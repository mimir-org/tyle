import {
  AspectObjectLibCm,
  AttributeLibCm,
  QuantityDatumLibCm,
  RdsLibCm,
  TerminalLibCm,
  UnitLibCm,
} from "@mimirorg/typelibrary-types";
import { ItemType } from "../../../entities/types/itemTypes";

export type SearchResult = ItemType;

export type SearchResultRaw =
  | AspectObjectLibCm
  | TerminalLibCm
  | AttributeLibCm
  | UnitLibCm
  | QuantityDatumLibCm
  | RdsLibCm;
