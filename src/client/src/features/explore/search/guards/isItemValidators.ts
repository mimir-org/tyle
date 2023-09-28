import {
  BlockLibCm,
  AttributeLibCm,
  QuantityDatumLibCm,
  TerminalLibCm,
  UnitLibCm,
  AttributeGroupLibCm,
  RdsLibCm,
} from "@mimirorg/typelibrary-types";
import { TerminalItem } from "../../../../common/types/terminalItem";
import { AttributeItem } from "../../../../common/types/attributeItem";
import { BlockItem } from "../../../../common/types/blockItem";
import { RdsItem } from "common/types/rdsItem";

export const isAttributeLibCm = (item: unknown): item is AttributeLibCm =>
  (<AttributeLibCm>item).kind === "AttributeLibCm";

export const isAttributeGroupLibCm = (item: unknown): item is AttributeGroupLibCm =>
  (<AttributeGroupLibCm>item).kind === "AttributeGroupLibCm";

export const isTerminalLibCm = (item: unknown): item is TerminalLibCm => (<TerminalLibCm>item).kind === "TerminalLibCm";

export const isTerminalItem = (item: unknown): item is TerminalItem => (<TerminalItem>item).kind === "TerminalItem";

export const isAttributeItem = (item: unknown): item is AttributeItem =>
  (<AttributeItem>item).kind === "AttributeLibCm";

export const isBlockLibCm = (item: unknown): item is BlockLibCm => (<BlockLibCm>item).kind === "BlockLibCm";

export const isBlockItem = (item: unknown): item is BlockItem => (<BlockItem>item).kind === "BlockItem";

export const isUnitLibCm = (item: unknown): item is UnitLibCm => (<UnitLibCm>item).kind === "UnitLibCm";

export const isQuantityDatumLibCm = (item: unknown): item is QuantityDatumLibCm =>
  (<QuantityDatumLibCm>item).kind === "QuantityDatumLibCm";

export const isRdsLibCm = (item: unknown): item is RdsLibCm => (<QuantityDatumLibCm>item).kind === "RdsLibCm";

export const isRdsItem = (item: unknown): item is RdsItem => (<QuantityDatumLibCm>item).kind === "RdsItem";
