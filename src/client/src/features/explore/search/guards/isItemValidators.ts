import {
  AspectObjectLibCm,
  AttributeLibCm,
  QuantityDatumLibCm,
  TerminalLibCm,
  UnitLibCm,
} from "@mimirorg/typelibrary-types";
import { TerminalItem } from "../../../../common/types/terminalItem";
import { AttributeItem } from "../../../../common/types/attributeItem";
import { AspectObjectItem } from "../../../../common/types/aspectObjectItem";

export const isAttributeLibCm = (item: unknown): item is AttributeLibCm =>
  (<AttributeLibCm>item).kind === "AttributeLibCm";

export const isTerminalLibCm = (item: unknown): item is TerminalLibCm => (<TerminalLibCm>item).kind === "TerminalLibCm";

export const isTerminalItem = (item: unknown): item is TerminalItem => (<TerminalItem>item).kind === "TerminalItem";

export const isAttributeItem = (item: unknown): item is AttributeItem =>
  (<AttributeItem>item).kind === "AttributeLibCm";

export const isAspectObjectLibCm = (item: unknown): item is AspectObjectLibCm =>
  (<AspectObjectLibCm>item).kind === "AspectObjectLibCm";

export const isAspectObjectItem = (item: unknown): item is AspectObjectItem =>
  (<AspectObjectItem>item).kind === "AspectObjectItem";

export const isUnitLibCm = (item: unknown): item is UnitLibCm => (<UnitLibCm>item).kind === "UnitLibCm";

export const isQuantityDatumLibCm = (item: unknown): item is QuantityDatumLibCm =>
  (<QuantityDatumLibCm>item).kind === "QuantityDatumLibCm";

export const isRdsLibCm = (item: unknown): item is QuantityDatumLibCm => (<QuantityDatumLibCm>item).kind === "RdsLibCm";

export const isRdsItem = (item: unknown): item is QuantityDatumLibCm => (<QuantityDatumLibCm>item).kind === "RdsItem";
