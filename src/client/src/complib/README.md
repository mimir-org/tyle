# Component library / Design system

## Purpose

This directory houses stories and components that can be consumed by the **:TYLE** client and potentially others if extracted into its own package.
By offering a collection of well-built components that conform to shared design tokens we can establish consistent styling and behaviour throughout the applications that utilizes them.

## Rules

- Every component that is available in this directory must offer a generic and reasonable api that is not domain specific.
- Every component must have a story associated with it, either directly or through its parent component which then is always used in combination with the child component.
- None of the components/files/logic in this directory should have dependencies on other items than what is already present in this directory or in the /assets directory.
