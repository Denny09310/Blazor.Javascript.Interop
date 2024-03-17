declare const DotNet: {
    invokeMethodAsync<T>(assemblyName: string, methodIdentifier: string, ...args: any[]): Promise<T>
    invokeMethod<T>(assemblyName: string, methodIdentifier: string, ...args: any[]): T
    attachReviver(reviver: (key: string, value: any) => any): void;
    createJSObjectReference(object: any): Record<string, object>;
    disposeJSObjectReference(id: number): void;
};