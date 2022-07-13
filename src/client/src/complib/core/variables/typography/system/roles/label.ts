import { math } from "polished";
import { typefaceReference } from "../../reference/typefaceReference";
import { typeScaleReference } from "../../reference/typeScaleReference";
import { NominalScale } from "../../types";
import { typeScaleSystem } from "../typeScaleSystem";

export const label: NominalScale = {
  large: {
    tracking: 0.1,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0.1 / ${typeScaleReference.size.n1} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0.5,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n2,
    letterSpacing: math(`0.5 / ${typeScaleReference.size.n2} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n2} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0.5,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n3,
    letterSpacing: math(`0.5 / ${typeScaleReference.size.n2} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n3} ${typefaceReference.brand}`,
  }
}