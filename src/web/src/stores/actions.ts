export interface ActionsWithoutPayload<TypeAction> {
  type: TypeAction
}

export function createAction<TypeAction>(actionType: TypeAction): () => ActionsWithoutPayload<TypeAction> {
  return (): ActionsWithoutPayload<TypeAction> => {
    return {
      type: actionType
    }
  }
}

export function createActionPayload<TypeAction, TypePayload>(
  actionType: TypeAction
): (payload: TypePayload) => ActionsWithPayload<TypeAction, TypePayload> {
  return (p: TypePayload): ActionsWithPayload<TypeAction, TypePayload> => {
    return {
      payload: p,
      type: actionType
    }
  }
}

export interface ActionsWithPayload<TypeAction, TypePayload> {
  type: TypeAction
  payload: TypePayload
}

interface ActionCreatorsMapObject {
  [key: string]: (...args: any[]) => ActionsWithPayload<any, any> | ActionsWithoutPayload<any>
}

export type ActionsUnion<A extends ActionCreatorsMapObject> = ReturnType<A[keyof A]>

export interface IAction {
  type: string
}
