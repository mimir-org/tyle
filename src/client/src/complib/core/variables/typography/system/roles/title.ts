import { typeScaleSystem } from "../typeScaleSystem";
import { typefaceReference } from "../../reference/typefaceReference";
import { math } from "polished";
import { NominalScale } from "../../types";
import { typeScaleReference } from "../../reference/typeScaleReference";

export const title: NominalScale = {
  large: {
    tracking: 0,
    size: typeScaleSystem.size.p1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p1,
    letterSpacing: math(`0 / ${typeScaleReference.size.p1} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p1} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0.15,
    size: typeScaleSystem.size.base,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.base,
    letterSpacing: math(`0.15 / ${typeScaleReference.size.base} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.base} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0.1,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0.1 / ${typeScaleReference.size.n1} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
}