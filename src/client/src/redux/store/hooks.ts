import { useMemo } from "react";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { RootState } from ".";
import { OutputParametricSelector, OutputSelector, ParametricSelector, Selector, createSelector } from "@reduxjs/toolkit";

type UnknownFunction = (...args: unknown[]) => unknown;
type RootStateSelector = Selector<RootState>;
type RootStateSelectorArray = ReadonlyArray<RootStateSelector>;

/**
 * Custom Dispatch hook used within application.
 */
export const useAppDispatch = useDispatch;

/**
 * Custom createSelector hook used within application, with type linting.
 */

export const createAppSelector: <R, T>(
  selector: Selector<RootState, R>,
  combiner: (res: R) => T
) => OutputSelector<RootStateSelectorArray, T, (res: R) => T> = createSelector;

/**
 * Custom createSelector hook used within application, with type linting, allowing for props as parameters.
 */
export const createParametricAppSelector: <R1, R2, P, T>(
  selector1: ParametricSelector<RootState, P, R1>,
  selector2: ParametricSelector<RootState, P, R2>,
  combiner: (res1: R1, res2: R2) => T
) => OutputParametricSelector<RootState, P, T, (res1: R1, res2: R2) => T> = createSelector;

/**
 * Custom createSelector hook used within application, with type linting, allowing for combining a list of selectors into a single selector.
 */
export const combineAppSelectors: <R, T>(
  selectors: Selector<RootState, R>[],
  combiner: (...res: R[]) => T
) => OutputSelector<RootStateSelectorArray, T, (...res: R[]) => T> = createSelector;

/**
 * Custom useSelector hook with type linting.
 */
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

/**
 * Custom hook to conveniently pass props as parameters to a selector.
 * Simplifies the following pattern:
 *
 * @example
 * const values = useAppSelector((state) => selector(state, props))
 *
 * into:
 *
 * @example
 * const values = useParametricAppSelector(selector, props);
 */
export const useParametricAppSelector = <P, R>(
  selector: OutputParametricSelector<RootState, P, R, UnknownFunction>,
  props: P
) => {
  return useAppSelector((state) => selector(state, props));
};

/**
 * Custom hook to conveniently pass props as parameters to a **unique** selector.
 * The hook ensures to properly memoize the selector, and should be used whenever a component uses a selector, and there exist multiple instances of the component.
 * Simplifies the following pattern:
 *
 * @example
 * const selector = useMemo(selectorFactoryFunc, [selectorFactoryFunc])
 * const values = useAppSelector((state) => selector(state, props))
 *
 * into:
 *
 * @example
 * const values = useUniqueParametricAppSelector(selectorFactoryFunc, props);
 */
export const useUniqueParametricAppSelector = <P, R>(
  selectorFactoryFunc: () => OutputParametricSelector<RootState, P, R, UnknownFunction>,
  props: P
) => {
  const selector = useMemo(selectorFactoryFunc, [selectorFactoryFunc]);

  return useAppSelector((state) => selector(state, props));
};
