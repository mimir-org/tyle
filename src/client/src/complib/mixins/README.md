# Mixins

## Purpose
This directory houses mixins that implement the property interfaces defined in the "./props" directory.  
The aim is that these should be reused for components that expose any of the properties defined within these interfaces.

In addition to the standard prop/mixin implementations one might find more specialized mixins such as "textVariantMixin.ts",
mixins like these are usually utilized by a small set of components.

## Partial implementations
Should you want to expose only parts of an interface to the consumer, then consider omitting or picking parts of a given interface.

### Example omit

In the following example all properties defined in the Sizing interface except maxWidth and maxHeight property will be available on the OmittingComponent.

```
interface MyOmittingInterface extends Omit<Sizing, "maxWidth" | "maxHeight"> {
    someAdditionalProperty?: string;
}

const OmittingComponent = styled.div<MyOmittingInterface>`
    ${sizingMixin};
    
    // Some custom property might follow
    color: ${(props) => props.someAdditionalProperty};
    
    // Other properties that are not exposed to the consumer could also be defined within
    font-size: 32px;
`;
```

### Example pick

In the following example all properties defined in the Typography interface in addition to the maxWidth and maxHeight properties from Sizing will be available on the PickyComponent.

```
interface MyPickyInterface extends Typography, Pick<Sizing, "maxWidth" | "maxHeight"> {
    someAdditionalProperty?: string;
}

const PickyComponent = styled.div<MyPickyInterface>`
    ${typographyMixin};
    ${sizingMixin};
    
    // Some custom property might follow
    color: ${(props) => props.someAdditionalProperty};
    
    // Other properties that are not exposed to the consumer could also be defined within
    font-size: 32px;
`;
```