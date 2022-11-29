# Common

## Purpose

This directory holds common components that are used across multiple features.  
The various items that are available in this directory are usually very application and library specific,  
and therefore would not belong in a generic component library either.

## Rules

- Components which are only used by a single feature should not reside within common/\*
- Components within common/\* shall have no dependencies on other features
