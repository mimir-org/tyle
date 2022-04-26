# Props

## Purpose
This directory houses interfaces that define common properties that can be exposed to the consumers of a given component.  
The interfaces draw inspiration from [MUI - System](https://mui.com/system/basics/) in what they contain and how they are separated from each other.

In addition to the common interfaces one might find more specialized interfaces such as "textVariant.ts", 
interfaces like these are usually shared between a small set of components.

## Partial implementations
If you want to expose only parts of an interface to the consumer then you should consider omitting parts the given interface.  

### Example omit
```
type MyPartialType = Omit<Sizing, "maxWidth" | "maxHeight">;

const exampleObject: MyPartialType = {
  // This object does not expose maxWidth and maxHeight
}
```