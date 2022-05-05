import { typeScaleSystem } from "../typeScaleSystem";
import { typefaceReference } from "../../reference/typefaceReference";
import { math } from "polished";
import { NominalScale } from "../../types";
import { typeScaleReference } from "../../reference/typeScaleReference";

export const body: NominalScale = {
  large: {
    tracking: 0.1,
    size: typeScaleSystem.size.base,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.base,
    letterSpacing: math(`0.1 / ${typeScaleReference.size.base} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.base} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0 / ${typeScaleReference.size.n1} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.n2,
    letterSpacing: math(`0 / ${typeScaleReference.size.n2} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.n2} ${typefaceReference.brand}`,
  },
}